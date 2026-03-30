import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { PaymentService } from './payment.service';
import { CommonModule } from '@angular/common';
import { Payment } from './payment.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

  payments: Payment[] = [];

  form!: FormGroup;

  editingId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private service: PaymentService
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      amount: [0, [Validators.required, Validators.min(1)]],
      currency: ['INR', Validators.required]
    });

    this.load(); // ✅ important
  }

  load() {
    this.service.getAll().subscribe({
      next: (res) => this.payments = res,
      error: (err) => console.error(err)
    });
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const payload: Payment = {
      ...this.form.value,
      clientRequestId: crypto.randomUUID()
    };

    this.service.create(payload).subscribe({
      next: () => {
        this.load();
        this.resetForm();
      },
      error: (err) => console.error(err)
    });
  }

  edit(p: Payment) {
    this.editingId = p.clientRequestId!;
    this.form.patchValue({
      amount: p.amount,
      currency: p.currency
    });
  }

  update() {
    if (!this.editingId) return;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
      const payload: Payment = {
    ...this.form.value,
    clientRequestId: this.editingId // ✅ VERY IMPORTANT
  };


    this.service.update(this.editingId,  payload)
      .subscribe({
        next: () => {
          this.load();
          this.editingId = null;
          this.resetForm();
        },
        error: (err) => console.error(err)
      });
  }

  delete(clientRequestId: string) {
    this.service.delete(clientRequestId).subscribe({
      next: () => this.load(),
      error: (err) => console.error(err)
    });
  }

  resetForm() {
    this.form.reset({
      amount: 0,
      currency: 'INR'
    });
  }
}