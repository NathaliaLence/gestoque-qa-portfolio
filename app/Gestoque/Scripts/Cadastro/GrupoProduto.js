//dados para o cadastro
function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#cbx_ativo').prop('checked', dados.Ativo);
}

//seta o foco no nome
function set_focus_form() {
    $('#txt_nome').focus();
}

//dados para inclusão
function get_dados_inclusao() {
    return {
        Id: 0,
        Nome: '',
        Ativo: true
    };
}

//obter os dados do form
function get_dados_form() {
    return {
        Id: $('#id_cadastro').val(),
        Nome: $('#txt_nome').val(),
        Ativo: $('#cbx_ativo').prop('checked')
    };
}

//preencher as linhas do grid
function preencher_linha_grid(param, linha) {
    linha
        .eq(0).html(param.Nome).end()
        .eq(1).html(param.Ativo ? 'SIM' : 'NÃO');
}