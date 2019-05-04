//objeto literal de logar funcionario
var index = {

    logar: function () {

        var dados = {
            cpf: fd.getValById("cpf"),
            senha: fd.getValById("senha")
        }

        if (dados.cpf.trim() == "" || dados.senha.trim() == "") {
            fd.getById("divMsg").className = "alert alert-danger";
            fd.getById("divMsg").innerHTML = "Preencha os campos!";
        }

        fd.getById("reqUsuario").style.display = "none";
        fd.getById("reqSenha").style.display = "none";

        if (dados.cpf == "") {
            fd.getById("reqUsuario").style.display = "inline";
        }

        if (dados.senha == "") {
            fd.getById("reqSenha").style.display = "inline";
        }

        if (dados.cpf == "" || dados.senha == "") {
            return;
        }


        fd.ajax("POST", "login/logar", dados, function (retornoServ) {

            //success

            if (retornoServ.operacao) {
                //redirect
                fd.getById("divMsg").className = "alert alert-success";
                fd.getById("divMsg").innerHTML = "Bem-vindo " + retornoServ.nome;

                setTimeout(function () {
                   // window.location.href = "home/index";
                }, 2000);
                
            }
            else {
                fd.getById("divMsg").className = "alert alert-danger";
                fd.getById("divMsg").innerHTML = "CPF ou senha inválidos!";
            }

        }, function () {//fail
            alert("Não foi possível processar sua requisição.");
        });
    }


}