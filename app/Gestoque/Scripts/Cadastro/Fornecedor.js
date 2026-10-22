//dados para o cadastro
function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#txt_num_documento').val(dados.NumDocumento);
    $('#txt_razao_social').val(dados.RazaoSocial);
    $('#txt_telefone').val(dados.Telefone);
    $('#txt_contato').val(dados.Contato);
    $('#txt_logradouro').val(dados.Logradouro);
    $('#txt_numero').val(dados.Numero);
    $('#txt_complemento').val(dados.Complemento);
    $('#txt_cep').val(dados.Cep);
    $('#txt_estado').val(dados.Estado);
    $('#txt_cidade').val(dados.Cidade);
    $('#cbx_ativo').prop('checked', dados.Ativo);
    $('#cbx_pessoa_juridica').prop('checked', false);
    $('#cbx_pessoa_fisica').prop('checked', false);

    if (dados.Tipo == 2) {
        $('#cbx_pessoa_juridica').prop('checked', true).trigger('click');
    }
    else {
        $('#cbx_pessoa_fisica').prop('checked', true).trigger('click');
    }
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
        NumDocumento: '',
        RazaoSocial: '',
        Tipo: 2,
        Telefone: '',
        Contato: '',
        Logradouro: '',
        Numero: '',
        Complemento: '',
        Cep: '',
        Estado: '',
        Cidade: '',
        Ativo: true
    };
}

//obter os dados do form
function get_dados_form() {
    return {
        Id: $('#id_cadastro').val(),
        Nome: $('#txt_nome').val(),
        NumDocumento: $('#txt_num_documento').val(),
        RazaoSocial: $('#txt_razao_social').val(),
        Tipo: $('#cbx_pessoa_juridica').is(':checked') ? 2 : 1,
        Telefone: $('#txt_telefone').val(),
        Contato: $('#txt_contato').val(),
        Logradouro: $('#txt_logradouro').val(),
        Numero: $('#txt_numero').val(),
        Complemento: $('#txt_complemento').val(),
        Cep: $('#txt_cep').val(),
        Estado: $('#txt_estado').val(),
        Cidade: $('#txt_cidade').val(),
        Ativo: $('#cbx_ativo').prop('checked')
    };
}

//preencher as linhas do grid
function preencher_linha_grid(param, linha) {
    linha
        .eq(0).html(param.Nome).end()
        .eq(1).html(param.Telefone).end()
        .eq(2).html(param.Contato).end()
        .eq(3).html(param.Ativo ? 'SIM' : 'NÃO');
}

$(document).ready(function ($) {
    $("#txt_telefone").mask("(00)0000-0000");
    $('#txt_cep').mask('00000-000');

})
    .on('click', '#cbx_pessoa_juridica', function () {
        $('label[for="txt_num_documento"]').text('CNPJ');
        $('#container_razao_social').removeClass('invisible');
    })
    .on('click', '#cbx_pessoa_fisica', function () {
        $('label[for="txt_num_documento"]').text('CPF');
        $('#container_razao_social').addClass('invisible');
    });