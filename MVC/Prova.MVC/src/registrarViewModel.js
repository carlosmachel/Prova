function RegistrarViewModel() {
    var self = this;

    self.result = ko.observable();    

    self.registroNome = ko.observable();
    self.registroEmail = ko.observable();
    self.registroSenha = ko.observable();
    self.registroConfirmacaoSenha = ko.observable();
    self.isLoading = ko.observable(false);

    self.errors = ko.observableArray([]);

    function showError(jqXHR) {
        self.isLoading(false);
        self.result(jqXHR.status + ': ' + jqXHR.statusText);
        var response = jqXHR.responseJSON;
        if (response) {
            if (response.codes) {
                var codes = response.codes;
                for (var i = 0; i < codes.length; i++) {
                    self.errors.push(codes[i])
                }
            }
        }
    }

    self.registrar = function () {
        self.isLoading(true);
        self.result('');
        self.errors.removeAll();

        var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();

        var data = {
            Nome: self.registroNome(),
            Email: self.registroEmail(),
            Senha: self.registroSenha(),
            ConfirmacaoSenha: self.registroConfirmacaoSenha(),
            __RequestVerificationToken: antiForgeryToken
        };

        $.ajax({
            type: 'POST',
            url: '/Account/Registrar',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.isLoading(false);
            window.location.href = data.Url;
        }).fail(showError);
    }    
}

ko.applyBindings( new RegistrarViewModel());