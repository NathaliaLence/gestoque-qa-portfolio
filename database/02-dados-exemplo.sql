-- ============================================================
-- Gestoque · dados de exemplo para testes
-- Usuários de TESTE (uso apenas local; senhas fictícias):
--   login           senha          perfil
--   gerente         Gerente@123    perfil Gerente
--   administrativo  Admin@123      perfil Administrativo
--   operador        Operador@123   perfil Operador
-- As senhas são gravadas em MD5, como o sistema original faz.
-- ============================================================

INSERT INTO [perfil] ([nome], [ativo]) VALUES
('Gerente', 1), ('Administrativo', 1), ('Operador', 1);
GO

INSERT INTO [usuario] ([login], [senha], [nome], [email]) VALUES
('gerente', '2d86d3f096e9d2c6350f54f3b9528f91', 'Gerente de Teste', 'gerente@gestoque.local'),
('administrativo', '0e7517141fb53f21ee439b355b5a1d0a', 'Administrativo de Teste', 'administrativo@gestoque.local'),
('operador', '862c83bf332c362bf53479cd2849a1c2', 'Operador de Teste', 'operador@gestoque.local');
GO

INSERT INTO [perfil_usuario] ([id_perfil], [id_usuario]) VALUES (1, 1), (2, 2), (3, 3);
GO

INSERT INTO [unidade_medida] ([nome], [sigla], [ativo]) VALUES
('Unidade', 'UN', 1), ('Caixa', 'CX', 1), ('Quilograma', 'KG', 1), ('Litro', 'L', 1);
GO

INSERT INTO [grupo_produto] ([nome], [ativo]) VALUES
('Material de escritório', 1), ('Limpeza', 1), ('Alimentos', 1), ('Informática', 0);
GO

INSERT INTO [marca_produto] ([nome], [ativo]) VALUES
('Faber', 1), ('Ypê', 1), ('Nestlé', 1), ('Genérica', 1);
GO

INSERT INTO [local_armazenamento] ([nome], [ativo]) VALUES
('Depósito central', 1), ('Prateleira A', 1), ('Prateleira B', 1);
GO

INSERT INTO [fornecedor] ([nome], [razao_social], [num_documento], [tipo], [telefone], [contato],
  [logradouro], [numero], [complemento], [cep], [estado], [cidade], [ativo]) VALUES
('Papelaria Central', 'Papelaria Central Ltda', '12.345.678/0001-90', 2, '(19) 3333-1000', 'Ana Souza',
  'Rua das Flores', '100', 'Sala 2', '13400-000', 'São Paulo', 'Piracicaba', 1),
('Distribuidora Limpa Bem', 'Limpa Bem Distribuidora S.A.', '98.765.432/0001-10', 2, '(19) 3333-2000', 'Carlos Lima',
  'Av. Independência', '2500', NULL, '13416-000', 'São Paulo', 'Piracicaba', 1),
('João da Silva', NULL, '123.456.789-00', 1, '(19) 99999-0000', 'João da Silva',
  'Rua do Porto', '45', NULL, '13400-100', 'São Paulo', 'Piracicaba', 1);
GO

-- Estoques variados: alguns abaixo de 10 para aparecer no relatório de ressuprimento,
-- um zerado e um produto inativo.
INSERT INTO [produto] ([codigo], [nome], [preco_custo], [preco_venda], [quant_estoque],
  [id_unidade_medida], [id_grupo], [id_marca], [id_fornecedor], [id_local_armazenamento], [ativo]) VALUES
('ESC001', 'Caneta esferográfica azul', 0.80, 2.50, 150, 1, 1, 1, 1, 2, 1),
('ESC002', 'Caderno universitário', 8.00, 19.90, 40, 1, 1, 4, 1, 2, 1),
('ESC003', 'Papel sulfite A4 (resma)', 18.00, 29.90, 6, 2, 1, 4, 1, 1, 1),
('LIM001', 'Detergente 500 ml', 1.90, 3.99, 80, 1, 2, 2, 2, 3, 1),
('LIM002', 'Água sanitária 1 L', 2.50, 5.49, 3, 4, 2, 2, 2, 3, 1),
('LIM003', 'Sabão em pó 1 kg', 9.00, 16.90, 0, 3, 2, 2, 2, 1, 1),
('ALI001', 'Café em pó 500 g', 12.00, 21.90, 25, 3, 3, 3, 3, 1, 1),
('ALI002', 'Leite em pó 400 g', 10.50, 18.90, 9, 3, 3, 3, 3, 1, 1),
('INF001', 'Mouse USB', 15.00, 39.90, 12, 1, 4, 4, 1, 2, 0);
GO
