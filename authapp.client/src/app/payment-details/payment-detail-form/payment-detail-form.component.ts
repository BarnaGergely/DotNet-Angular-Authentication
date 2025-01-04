import { Component } from '@angular/core';
import { PaymentDetailService } from '../../shared/payment-detail.service';
import { FormsModule, NgForm } from '@angular/forms';
import { PaymentDetail } from '../../shared/payment-detail.model';

@Component({
  selector: 'app-payment-detail-form',
  imports: [FormsModule],
  templateUrl: './payment-detail-form.component.html',
  styleUrl: './payment-detail-form.component.css'
})
export class PaymentDetailFormComponent {
  constructor(public paymentDetailService: PaymentDetailService) {
  }
  onSubmit(form: NgForm) {
    if (this.paymentDetailService.formData.paymentDetailId === 0) {
      this.insertRecord(form);
    } else {
      this.updateRecord(form);
    }
  }

  insertRecord(form: NgForm) {
    this.paymentDetailService.postPaymentDetail().subscribe({
      next: (response) => {
        console.log(response as PaymentDetail);
        this.paymentDetailService.resetForm(form);
        this.paymentDetailService.refreshList();
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  updateRecord(form: NgForm) {
    this.paymentDetailService.putPaymentDetail().subscribe({
      next: (response) => {
        console.log(response as PaymentDetail);
        this.paymentDetailService.resetForm(form);
        this.paymentDetailService.refreshList();
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}
