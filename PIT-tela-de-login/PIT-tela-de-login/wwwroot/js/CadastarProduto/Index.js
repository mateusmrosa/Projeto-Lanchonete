var index = {

    cadastrar: function () {


        var dados = {
            id: fd.getValById("hId"),
            nome: fd.getValById("nome"),
            tipoProd: fd.getValById("tipoProd"),
            valor: fd.getValById("valor")
        }

        if (dados.nome.trim() == "" || dados.tipoProd.trim() == "" || dados.valor.trim() == "") {

            alert("Todos os campos são obrigatórios.");
            return;
        }

        fd.ajax("POST", "CadastrarProduto/Gravar", dados, function (retornoServ) {

            if (retornoServ.operacao) {

                alert(retornoServ.msg);
            }
            else {

                alert(retornoServ.msg);
            }


        }, function () {

            alert("Não foi possível salvar. Tente novamente.");
        });
    }
}