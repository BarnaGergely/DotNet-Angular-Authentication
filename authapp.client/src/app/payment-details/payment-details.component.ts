import { Component, OnInit } from '@angular/core';
import { PaymentDetailService } from '../shared/payment-detail.service';
import { NgFor } from '@angular/common';
import { PaymentDetailFormComponent } from "./payment-detail-form/payment-detail-form.component";
import { PaymentDetail } from '../shared/payment-detail.model';

@Component({
    selector: 'app-payment-details',
    templateUrl: './payment-details.component.html',
    styleUrl: './payment-details.component.css',
    imports: [NgFor, PaymentDetailFormComponent]
})
export class PaymentDetailsComponent implements OnInit {
    constructor(public paymentDetailService: PaymentDetailService) { }
    ngOnInit(): void {
        this.paymentDetailService.refreshList();
    }
    populateForm(selectedRecord: PaymentDetail) {
        this.paymentDetailService.formData = Object.assign({}, selectedRecord);
    }
    onDelete(id: number) {
        this.paymentDetailService.deletePaymentDetail(id).subscribe({
            next: (response) => {
                console.log(response);
                this.paymentDetailService.refreshList();
            },
            error: (err) => {
                console.error(err);
            }
        });
    }
}
