function AlterarSenhaViewModel() {
    var self = this;
    
    self.senhaAntiga = ko.observable();
    self.senhaNova = ko.observable();
    self.confirmacaoSenha = ko.observable();
    self.result = ko.observable();
    self.errors = ko.observableArray([]);
    self.isLoading = ko.observable();

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

    self.enviar = function () {
        self.isLoading(true);
        var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
        var data = {            
            SenhaAntiga: self.senhaAntiga(),
            SenhaNova: self.senhaNova(),
            ConfirmacaoSenha: self.confirmacaoSenha(),
            __RequestVerificationToken: antiForgeryToken
        }

        $.ajax({
            type: 'POST',
            url: '/Account/AlterarSenha',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.isLoading(false);
            window.location.href = data.Url;
        }).fail(showError);
    }

}

ko.applyBindings(new AlterarSenhaViewModel());