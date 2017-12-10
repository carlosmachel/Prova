function LoginViewModel() {
    var self = this;

    self.result = ko.observable();
    self.user = ko.observable();

    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();
    self.errors = ko.observableArray([]);
    self.isLoading = ko.observable(false);

    self.esqueciMinhaSenha = function () {
        window.location.href = "/Account/EsqueciSenha";
    }

    self.criarConta = function () {
        window.location.href = "/Account/Registrar";
    }

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

    self.login = function () {
        self.isLoading(true);
        self.result('');
        self.errors.removeAll();
        var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();

        var loginData = {
            grant_type: 'password',
            email: self.loginEmail(),
            senha: self.loginPassword(),
            __RequestVerificationToken: antiForgeryToken          
        };

        $.ajax({
            type: 'POST',
            url: '/Account/Login',
            data: loginData
        }).done(function (data) {
            self.isLoading(false);
            window.location.href = data.Url;
        }).fail(showError);
    }
}

ko.applyBindings(new LoginViewModel());