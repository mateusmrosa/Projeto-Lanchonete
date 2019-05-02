//objeto literal
var index = {

    gravar: function () {

        var obj = {
            id: fd.getValById("hId"),
            nomeCompleto: fd.getValById("nome"),
            cpf: fd.getValById("cpf"),
            senha: fd.getValById("senha")
        }

        if (obj.nomeCompleto.trim() == "" || obj.cpf.trim() == "" || obj.senha.trim() == "") {
            fd.getById("divMsg").className = "alert alert-danger";
            fd.getById("divMsg").innerHTML = "Preencha os campos!";
            return;
        }

        fd.ajax("POST", "/CadastrarFuncionario/Gravar", obj, index.gravarSuccess, index.gravarFail);
    },

    gravarSuccess: function (retornoServer) {
        fd.getById("divMsg").className = "";
        fd.getById("divMsg").innerHTML = retornoServer.msg;
        fd.getById("hId").value = retornoServer.id;
    },

    gravarFail: function () {
        alert("deu erro na requisição");
    },

    cancelar: function () {
        window.location.href = window.location.href;
    },

    abrirPesquisa: function () {
        
        //alert("oi");
        $.fancybox.open({
            src: "/cadastrarfuncionario/IndexPesquisar",
            'height': '400px',
            'width': '400px',
            type: 'iframe',
            smallBtn: true
        });
    },

    editar: function (id) {

        $.fancybox.close();

        var url = "/GerenciadorFuncionario/obter/" + id
        fd.ajax("GET", url, null, function (retServ) {

            fd.getById("hId").value = retServ.id;
            fd.getById("txtNome").value = retServ.nomeCompleto;
            fd.getById("txtCPF").value = retServ.cpf;
            fd.getById("txtSenha").disabled = "disabled";

        }, function () {

            alert("Não foi possível obter o funcionário.");
        });

    }
}