function EsqueciSenhaViewModel() {
    var self = this;

    self.email = ko.observable();    
    self.isLoading = ko.observable(false);

    function recuperarSenha() {
        self.isLoading(true);
        var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
        var data = {
            Email: self.email(),
            __RequestVerificationToken: antiForgeryToken,
            Captcha: grecaptcha.getResponse()
        };

        $.ajax({
            type: 'POST',
            url: '/Account/EsqueciSenha',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.isLoading(false);
            window.location.href = data.Url;
        }).fail(function () {
            self.isLoading(false);
            window.location.href = data.Url;
        });

    }

    self.recuperarSenha = function () {
        recuperarSenha();
    }   
}

ko.applyBindings(new EsqueciSenhaViewModel());
