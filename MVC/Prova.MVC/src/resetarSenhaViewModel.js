function ResetarSenhaViewModel(model) {
    var self = this;
    
    self.resetarCode = ko.observable(model.Code);
    self.resetarEmail = ko.observable(model.Email);
    self.resetarSenha = ko.observable();
    self.resetarSenhaConfirmacao = ko.observable();
    self.isLoading = ko.observable(false);

    self.result = ko.observable();
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

    self.enviar = function () {
        self.isLoading(true);
        var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
        var data = {
            Email: self.resetarEmail(),
            Senha: self.resetarSenha(),
            ConfirmacaoSenha: self.resetarSenhaConfirmacao(),
            Code : self.resetarCode(),
            __RequestVerificationToken: antiForgeryToken
        }

        $.ajax({
            type: 'POST',
            url: '/Account/ResetarSenha',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.isLoading(false);
            window.location.href = data.Url;
        }).fail(showError);
    }
}

$(document).ready(function () {
    var data = JSON.parse($("#serverJSON").val());
    ko.applyBindings(new ResetarSenhaViewModel(data));
});