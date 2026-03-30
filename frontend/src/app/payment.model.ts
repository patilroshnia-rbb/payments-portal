export interface Payment {
  id?: string;
  reference?: string;
  amount: number;
  currency: string;
  createdAt?: string;
  clientRequestId: string;
}