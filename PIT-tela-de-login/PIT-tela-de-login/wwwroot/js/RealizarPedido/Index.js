var index = {

    dados: [],

    init: function () {

        document.body.onclick = function () {
            var ulAutoCompleteProduto = fd.getById("ulAutoCompleteProduto");
            //ulAutoCompletePatrimonio.innerHTML = "";
            ulAutoCompleteProduto.style.display = "none";
        }
    },


    pesquisarProduto: function () {

        var ulAutoCompleteProduto = fd.getById("ulAutoCompleteProduto");

        if (fd.getValById("txtProduto").trim().length <= 2) {
            ulAutoCompleteProduto.innerHTML = "";
            ulAutoCompleteProduto.style.display = "none";
            fd.getById("hIdProduto").value = "";
            fd.getById("btnAdd").classList.add("disabled");
            return;
        }

        var url = "/RealizarPedido/PesquisarProduto?termo=" + fd.getValById("txtProduto");

        fd.ajax("GET", url, null, function (retServ) {

            var lis = "";
            for (var i = 0; i < retServ.length; i++) {

                lis += "<li onclick=\"index.obterProduto(" + retServ[i].id + ")\" data-id=\"" + retServ[i].id + "\">" + retServ[i].nome + "</li>"
            }
            ulAutoCompleteProduto.innerHTML = lis;

            if (retServ.length > 0)
                ulAutoCompleteProduto.style.display = "block";
            else {
                ulAutoCompleteProduto.style.display = "none";
                fd.getById("hIdProduto").value = "";
            }

        }, function () {
            alert("deu erro....");
        });

    },


    obterProduto: function (id) {

        var li = document.querySelector("li[data-id='" + id + "']")
        var id = li.getAttribute("data-id");
        var nome = li.innerHTML;

        fd.getById("hIdProduto").value = id;
        fd.getById("txtProduto").value = nome;
        fd.getById("btnAdd").classList.remove("disabled");

    },


    adicionar: function () {

        if (fd.getById("hIdProduto").value == "0" || fd.getById("hIdProduto").value == "") {

            alert("Selecione um patrimônio.");
            return;
        }

        if (fd.getById("txtQuantidade").value.trim() == "") {

            alert("Informe a quantidade do produto no pedido.");
            return;
        }

        if (fd.getById("txtValor").value.trim() == "") {
            alert("Informe o valor do item");
            return;
        }

        var item = {
            produtoId: fd.getById("hIdProduto").value,
            nome: fd.getById("txtProduto").value,
            valor: fd.getById("txtValor").value,
            quantidade: fd.getById("txtQuantidade").value,
            edicao: false
        };

        var pos = index.dados.findIndex(function (linha) {
            if (linha.produtoId == item.produtoId) {
                return true;
            }
        });

        if (pos > -1) {

            if (!index.dados[pos].edicao)
                alert("Este item já foi incluído.");
            else {
                index.dados[pos] = item;
                index.carregarItems();
            }
        }
        else {
            index.dados.push(item);

            fd.getById("hIdProduto").value = "";
            fd.getById("txtQuantidade").value = "";
            fd.getById("txtValor").value = "";
            fd.getById("txtProduto").value = "";
            fd.getById("btnAdd").classList.add("disabled");

            index.carregarItems();
        }


    },

    carregarItems: function () {

        var tbodyItems = fd.getById("tbodyItems");
        tbodyItems.innerHTML = "";
        var trs = "";
        index.dados.forEach(function (linha) {

            trs += "<tr data-id=\"" + linha.produtoId + "\" > " +
                "  <td>" + linha.nome + "</td>" +
                "  <td>" + linha.quantidade + "</td>" +
                "  <td>" + "R$ " + linha.valor + "</td>" +
                "  <td><a onclick=\"index.excluirItem(" + linha.produtoId + ")\" class=\"icon-cancel\"></a>" +
                "      <a onclick=\"index.editarItem(" + linha.produtoId + ")\"class=\"icon-pencil\"></a>" +
                "   </td > " +
                "</tr > "
        });

        tbodyItems.innerHTML = trs;

    },

    excluirItem(id) {

        if (!confirm("Tem certeza?")) {
            return;
        }

        var pos = index.dados.findIndex(function (linha) {

            if (linha.produtoId == id) {
                return true;
            }
        });

        if (pos > -1) {

            index.dados.splice(pos, 1);
            index.carregarItems();
        }

    },


    editarItem(id) {

        var item = index.dados.find(function (linha) {

            if (linha.produtoId == id) {
                linha.edicao = true;
                return true;
            }
        });

        if (item != null) {

            fd.getById("hIdProduto").value = item.produtoId;
            fd.getById("txtQuantidade").value = item.quantidade;
            fd.getById("txtValor").value = item.valor;
            fd.getById("txtProduto").value = item.nome;
            fd.getById("btnAdd").classList.remove("disabled");

        }
    },

    gravar() {

        var dados = {
            id: fd.getValById("hId"),
            idfunc: fd.getValById("hIdFunc"),
            items: index.dados
        };

        //if (fd.getById("txtProduto").value.trim() == "" || fd.getById("txtQuantidade").value.trim() == "" || fd.getById("txtValor").value.trim() == "") {
        //    fd.getById("divMsg").className = "alert alert-danger";
        //    fd.getById("divMsg").innerHTML = "Preencha so campo do pedido!";
        //    return;
            
        //}

            fd.ajax("POST", "/RealizarPedido/Gravar", dados, function (retServ) {

                if (retServ.operacao) {
                    fd.getById("divMsg").className = "alert alert-success";
                    fd.getById("divMsg").innerHTML = "Pedido cadastrado com sucesso!";
                }
                else {

                    alert("Não foi possível gravar.");
                }

            }, function () {



            });
    },

    novo: function () {
        window.location.href = window.location.href;
    }


}


document.addEventListener("DOMContentLoaded", function () {

    index.init();
});