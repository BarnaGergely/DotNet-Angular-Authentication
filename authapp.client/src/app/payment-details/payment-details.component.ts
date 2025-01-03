import { Component, OnInit } from '@angular/core';
import { PaymentDetailService } from '../shared/payment-detail.service';

@Component({
    selector: 'app-payment-details',
    templateUrl: './payment-details.component.html',
    styleUrl: './payment-details.component.css',
})
export class PaymentDetailsComponent implements OnInit {
 constructor(private paymentDetailService: PaymentDetailService) {
 }
    ngOnInit(): void {
        this.paymentDetailService.refreshList();
    }
}
