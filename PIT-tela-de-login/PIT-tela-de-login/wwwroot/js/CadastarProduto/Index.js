var index = {

    cadastrar: function () {


        var dados = {
            id: fd.getValById("hId"),
            nome: fd.getValById("nome"),
            tipoProd: fd.getValById("tipoProd"),
            quantidade: fd.getValById("quantidade"),
            valor: fd.getValById("valor")
        }

        if (dados.nome.trim() == "" || dados.tipoProd.trim() == "" || dados.valor.trim() == ""
            || dados.quantidade.trim() == "") {
            fd.getById("divMsg").className = "alert alert-danger";
            fd.getById("divMsg").innerHTML = "Preencha os campos!";
            return;
           
            //alert("Todos os campos são obrigatórios.");
            //return;
        }

        fd.ajax("POST", "CadastrarProduto/Gravar", dados, function (retornoServ) {

            if (retornoServ.operacao) {
                fd.getById("divMsg").className = "alert alert-success";
                fd.getById("divMsg").innerHTML = "Produto cadastrado com sucesso!";
                fd.getById("hId").value = retornoServ.id;
                //alert(retornoServ.msg);
            }
            else {

                alert(retornoServ.msg);
            }


        }, function () {

            alert("Não foi possível salvar. Tente novamente.");
        });
    },

    abrirPesquisa: function () {

        //alert("oi");
        $.fancybox.open({
            src: "/cadastrarproduto/IndexPesquisar",
            type: 'iframe',
            smallBtn: true
        });
    },

    cancelar: function () {
        window.location.href = window.location.href; 
    }
}