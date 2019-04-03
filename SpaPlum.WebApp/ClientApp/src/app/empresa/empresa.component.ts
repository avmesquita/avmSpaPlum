import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-empresa',
  templateUrl: './empresa.component.html'
})
export class EmpresaComponent {
  public empresas: Empresa[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Empresa[]>('https://localhost:44387/api/empresa').subscribe(result => {
      this.empresas = result;
    }, error => console.error(error));
  }
}

interface Empresa {
  CodigoEmpresa: string;
  Nome: string;
  RazaoSocial: string;
  CNPJ: string;
  Email: string;
  Telefone: string;
  Ativo: string;
  DataCadastro: Date;
  CodigoPlano: string;
}
