function Contato(id, nome, telefone, email) {
    var self = this;
    
    self.Id = ko.observable(id);
    self.Nome = ko.observable(nome).extend({ required: { params: true, message: "Campo Obrigatório", } });
    self.Telefone = ko.observable(telefone).extend({ required: { params: true, message: "Campo Obrigatório", } });
    self.Email = ko.observable(email).extend({ required: { params: true, message: "Campo Obrigatório", } });
    self.Mode = ko.observable("display");
    self.isValid = function () {
        return self.errors().length === 0;
    }

    self.Editar = function () {
        self.Mode("edit");
    };
    self.Salvar = function () {
        
    }

    self.Cancelar = function () {
        self.Mode("display");
    }

    self.errors = ko.validation.group(self);    
}

function ContatosViewModel() {
    var self = this;

    self.desabilitarAcoes = ko.observable(false);   

    self.contatos = ko.observableArray([]);
    self.editContato = ko.observable();
    
    self.criar = function () {
        var contato = new Contato();
        contato.Mode("edit");
        contato.Id(0);
        self.desabilitarAcoes(true);
        self.contatos.push(contato);
    }

    function cloneContato(observableObject) {
        return ko.mapping.fromJS(ko.toJS(observableObject));
    }

    function removeData(currentData) {
        $.ajax({
            type: "DELETE",            
            url: '/Contato/Remover/' + currentData.Id()            
        }).done(function () { self.contatos.remove(currentData);
        }).error(function (ex) {
            alert("ERROR Saving");
        })                       
    }

    function saveData(currentData) {
        var postUrl = "";
        var submitData = {
            Id: currentData.Id(),
            Nome: currentData.Nome(),
            Telefone: currentData.Telefone(),
            Email: currentData.Email(),

        };
        if (currentData.Id() && currentData.Id() > 0) {
            postUrl = "/Contato/Editar"
        }
        else {
            postUrl = "/Contato/Criar"
        }

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: postUrl,
            data: JSON.stringify(submitData)
        }).done(function (id) {
            currentData.Id(id);
        }).error(function (ex) {
            alert("ERROR Saving");
        })
    }

    self.editar = function (currentData) {
        currentData.Editar();

        self.desabilitarAcoes(true);
        self.editContato = cloneContato(currentData);
    }

    self.cancelar = function (currentData) {
        if (currentData.Id() === 0) {
            self.contatos.remove(currentData);
        }
        else {
            currentData.Nome(self.editContato.Nome());
            currentData.Telefone(self.editContato.Telefone());
            currentData.Email(self.editContato.Email());

            self.editContato = null;

            currentData.Cancelar();
        }

        self.desabilitarAcoes(false);
    }

    self.remover = function (currentData) {        
        removeData(currentData);
    }

    self.salvar = function(currentData) {
        if (currentData.isValid()) {
            saveData(currentData);
            currentData.Mode("display");
            self.desabilitarAcoes(false);
        } else {
            currentData.errors.showAllMessages();
        }
    }

    $(document).ready(function () {
        var data = JSON.parse($("#serverJSON").val());
        $(data).each(function (index, element) {
            self.contatos.push(new Contato(element.Id, element.Nome, element.Telefone, element.Email));
        });
    });
}

ko.applyBindings(new ContatosViewModel());

ko.bindingHandlers.mask = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var mask = valueAccessor() || {};
        $(element).inputmask({ "mask": mask, 'autoUnmask': false });
        ko.utils.registerEventHandler(element, 'focusout', function () {
            var value = $(element).inputmask('unmaskedvalue');
            if (!value) {
                viewModel[$(element).attr("id")]("");
            }
        });
    }
};