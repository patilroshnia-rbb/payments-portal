import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Payment } from './payment.model';

@Injectable({ providedIn: 'root' })
export class PaymentService {

  api = 'https://localhost:7191/api/payments';

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Payment[]>(this.api);
  }

  create(p: Payment) {
    return this.http.post<Payment>(this.api, p);
  }

  update(id: string, p: Payment) {
    return this.http.put(`${this.api}/${id}`, p);
  }

  delete(id: string) {
    return this.http.delete(`${this.api}/${id}`);
  }
}