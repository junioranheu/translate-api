# Translate.API

A ideia inicial era criar uma API em .NET 8 estritamente utilizando APENAS o VS Code no MacOS. Mas a experiência foi péssima: complicado demais referenciar as camadas, foi super chato instalar as dependências, tudo é via terminal, simplesmente importar classes de um arquivo pro outro é algo complicado, e a cereja do bolo: quando rodei a API no VS, vi que o .sln estava todo quebrado!

Depois passei pro VS do MacOS: ruim (comparado ao do Windows).

Mas enfim, terminei (o básico do básico...) a API no Windows mesmo.

<b>>>>>>>>>>>></b> No final das contas, esse projeto foi super útil porque aprendi uma nova arquitetura/prática seguindo o tutorial de <b>arquitetura CQRS com MediatR</b> do Balta. E também, claro, utilizei <b>Docker</b>.

PS: A API tecnicamente se tratava sobre traduzir frases, etc. Mas isso não foi implementado, de fato. Só um CRUD (não literalmente C, R, U, D) das entidades Usuario, UsuarioRole, Role, Log e Frase.
