
//objeto literal
var indexPesquisar = {


    pesquisar: function () {

        var tbodyRes = fd.getById("tbodyRes");

        if (fd.getValById("nome").length <= 3) {
            fd.getById("tabResultado").style.display = "none";
            //tbodyRes.innerHTML = "<tr><td colspan=\"3\">Dados não encontrado</td></tr>";
            return;
        }
        var url = "Pesquisar?nome=" +
            fd.getValById("nome");


        fd.ajax("GET", url, null, function (retServ) {



            var linhas = "";//"<tr><td colspan=\"2\">Dados não encontrado</td></tr>";

            //***primeira forma, usando concatenação de strings
            //for (var i = 0; i < retServ.length; i++) {

            //    if (i == 0)
            //        linhas = "";
            //    linhas += "<tr><td>" + retServ[i].nomeCompleto + "</td><td>" + retServ[i].cpf + "</td></tr>";
            //}

            //retServ.forEach(function (item, index) {

            //    if (index == 0)
            //        linhas = "";

            //    linhas += "<tr><td>" + item.nomeCompleto + "</td><td>" + item.cpf + "</td></tr>";

            //});
            //tbodyRes.innerHTML = linhas;


            //segunda forma, manipulando a árvore DOM

            tbodyRes.innerHTML = "";
            if (retServ.length == 0) {
                fd.getById("tabResultado").style.display = "none";
                //tbodyRes.innerHTML = "<tr><td colspan=\"3\">Dados não encontrado</td></tr>";
            }
            else fd.getById("tabResultado").style.display = "table";

            retServ.forEach(function (item, index) {

                var tr = document.createElement("tr");
                tr.setAttribute("data-id", item.id);

                var tdNome = document.createElement("td");
                tdNome.innerHTML = item.nomeCompleto;

                var tdCPF = document.createElement("td");
                tdCPF.innerHTML = item.cpf;

                var tdAcoes = document.createElement("td")
                tdAcoes.innerHTML = "<a onclick=\"indexPesquisar.editar(" + item.id + ")\">editar</a>";
                tdAcoes.innerHTML += "<a onclick=\"indexPesquisar.excluir(" + item.id + ")\">excluir</a>";

                tr.appendChild(tdNome);
                tr.appendChild(tdCPF);
                tr.appendChild(tdAcoes);

                tbodyRes.appendChild(tr);

            });




        }, function () {

            alert("deu erro na consulta");

        });


    },

    editar: function (id) {

        //janela que abrir a pesquisa
        window.parent.indexf.editar(id);
    },

    excluir: function (id) {

        if (!confirm("Deseja excluir?")) {
            return;
        }

        var dados = {
            id: id
        };

        fd.ajax("POST", "/GerenciadorFuncionario/Excluir", dados, function (retServ) {

            if (retServ.operacao) {

                var trExcluir = document.querySelector("tr[data-id='" + id + "']");

                trExcluir.parentNode.removeChild(trExcluir);
            }


        }, function () {

            alert("Não foi possível excluir.")

        });


    }


}




