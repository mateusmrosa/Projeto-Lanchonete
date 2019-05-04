

var index = {

    gravarCliente: function () {

        var obj = {
            nome: fd.getValById("nome"),
            cpf: fd.getValById("cpf")
        }

        if (obj.nome.trim() == "" || obj.cpf.trim() == "") {
            fd.getById("divMsg").className = "alert alert-danger";
            fd.getById("divMsg").innerHTML = "Preencha os campos!";
            return;
        }

        fd.ajax("POST", "/cadastrarcliente/Gravar", obj, index.gravarSuccess, index.gravarFail);
    },

    gravarSucess: function (retornoServer) {
        fd.getById("divMsg").className = "";
        fd.getById("divMsg").innerHTML = retornoServer.msg;
        fd.getById("hId").value = retornoServer.id;
    },

    gravarFail: function () {
        alert("***ERROR***");
    },

    cancelarCliente: function () { 
        window.location.href = window.location.href;
    }
}