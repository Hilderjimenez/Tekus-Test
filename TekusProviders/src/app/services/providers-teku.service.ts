import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Provider } from '../models/provider.model';

@Injectable({
  providedIn: 'root'
})


export class ProvidersTekuService {

  private apiUrl = `${environment.apiUrl}/providersTekus`;

  constructor(private http: HttpClient) { }

  // Obtiene todos los proveedores
  getProvidersTekus(): Observable<Provider[]> {
    return this.http.get<Provider[]>(this.apiUrl);
  }

  getProviderTekus(NIT: string): Observable<Provider> {
    return this.http.get<Provider>(`${this.apiUrl}/${NIT}`);
  }

  addProvider(provider: Provider): Observable<Provider> {
    return this.http.post<Provider>(this.apiUrl, provider);
  }

  // Actualiza un proveedor existente
  updateProvider(NIT: string, provider: Provider): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${NIT}`, provider);
  }

  // Inactiva un proveedor
  inactivateProvider(NIT: string): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${NIT}/inactivate`, {});
  }

  // Activa un proveedor
  activateProvider(NIT: string): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${NIT}/activate`, {});
  }
}
