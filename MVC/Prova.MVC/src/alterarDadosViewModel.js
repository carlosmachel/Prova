function AlterarDadosViewModel(model) {
    var self = this;

    self.alterarDadosNome = ko.observable(model.Nome);
    self.alterarDadosEmail = ko.observable(model.Email);
    self.errors = ko.observable();
    self.formVisivel = ko.observable(true);
    self.confirmacao = ko.observable(false);
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
            Nome: self.alterarDadosNome(),                        
            __RequestVerificationToken: antiForgeryToken
        }

        $.ajax({
            type: 'POST',
            url: '/Account/AlterarDados',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            if (data.Sucesso) {
                self.formVisivel(false);
                self.confirmacao(true);
                self.isLoading(false);
            }
        }).fail(showError);
    }    
}

$(document).ready(function () {
    var data = JSON.parse($("#serverJSON").val());
    ko.applyBindings(new AlterarDadosViewModel(data));
});