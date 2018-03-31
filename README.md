# CSharp-KeyLogger
<ul>
  <li>Keylogger feito em c# que grava os dados digitados no teclado em um log.</li>
  <li>Envia todos os dados do log por email.</li>
  <li>Funciona como processo do Windows para que o usuario não perceba nenhuma mudança.</li>
</ul>

<p><b>Requer <a href="https://myaccount.google.com/lesssecureapps">autorização da GMail</a> para que a conexão ao Email seja realizada !</b></p>

<h2>Modo de usar</h2> 
<ul>
  <li>Na raiz do arquivo, vá no arquivo Email.cs.</li>
  <li>Modifique as linhas: 13, 14, 15, com seus dados.</li>
  <li>Compile e execute o arquivo taskhost.exe no local \bin\Debug.</li>
  <li>Configure o computador para que execute o programa ao ligar.</li>
</ul>
<h1></h1>
<h3>Observações</h3>
<ul>
	<li><b>O computador automaticamente limpara o log ao te enviar pelo email.</b></li>
	<li><b>Caso o computador não esteja conectado ae internet, ele aguarda conectar para que seja enviado o log.</b></li>
</ul>
