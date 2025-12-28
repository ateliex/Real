#language: pt-br

Funcionalidade: Apuração Mensal de Finanças por Categoria

Cenário de Fundo:
	Dado que existe as seguintes categorias de receitas:
		| Nome     |
		| Salário  |
	Dado que existe as seguintes categorias de despesas:
		| Nome     |
		| Roupa    |
		| Mercado  |
		| Refeição |
	E que existe as seguintes contas de crédito a pagar:
		| Nome      |
		| Ca Nubank |
		| Cartão C6 |
	E que existe as seguintes contas de débito:
		| Nome      |
		| Carteira_ |

Regra: A apuração mensal de finanças por categoria pode ser feita em regime de competência

Cenário: Apurar finanças por categoria do mês em regime de competência
	Dado que existe as seguintes finanças à vista:
		| Conta     | Competência | Data       | Categoria | Descrição    | Valor  | Tipo    |
		| Carteira_ | 02/02/2025  | 02/02/2025 | Salário   | Depósito   _ | 200,00 | Receita |
	E que existe as seguintes finanças a prazo:
		| Conta     | Competência | Data       | Categoria | Descrição    | Valor  | Tipo    |
		| Cartão C6 | 28/12/2024  | 10/03/2025 | Roupa     | Renner       | -74,90 | Despesa |
		| Cartão C6 | 04/02/2025  | 10/03/2025 | Mercado   | Fastmarket   | -09,49 | Despesa |
		| Cartão C6 | 13/02/2025  | 10/03/2025 | Mercado   | Supermercado | -49,87 | Despesa |
		| Cartão C6 | 06/02/2025  | 10/03/2025 | Refeição  | Almoço - Bar | -38,00 | Despesa |
	Quando eu apurar as finanças por categoria do mês de 'dezembro' de 2024 em regime de competência
	Então a apuração mensal de finanças por categoria deverá ter 0,00 de receitas
	E a apuração mensal de finanças por categoria deverá ter -74,90 de despesas
	E a apuração mensal de finanças por categoria deverá ter -74,90 de saldo
	E a apuração mensal de finanças por categoria deverá ter 0,00 de saldo acumulado
	E a apuração mensal de finanças por categoria deverá ter -74,90 de saldo total
	E a apuração mensal de finanças por categoria deverá ter as seguintes receitas:
		| Nome | Total |
	E a apuração mensal de finanças por categoria deverá ter as seguintes despesas:
		| Nome  | Total  |
		| Roupa | -74,90 |
	Quando eu apurar as finanças por categoria do mês de 'fevereiro' de 2025 em regime de competência
	Então a apuração mensal de finanças por categoria deverá ter 200,00 de receitas
	E a apuração mensal de finanças por categoria deverá ter -97,36 de despesas
	E a apuração mensal de finanças por categoria deverá ter 102,64 de saldo
	E a apuração mensal de finanças por categoria deverá ter -74,90 de saldo acumulado
	E a apuração mensal de finanças por categoria deverá ter 27,74 de saldo total
	E a apuração mensal de finanças por categoria deverá ter as seguintes receitas:
		| Nome    | Total  |
		| Salário | 200,00 |
	E a apuração mensal de finanças por categoria deverá ter as seguintes despesas:
		| Nome     | Total  |
		| Mercado  | -59,36 |
		| Refeição | -38,00 |
