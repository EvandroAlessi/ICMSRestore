import { Component, OnInit, Input } from '@angular/core';
import {  Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { InvoiceService } from '../../../services/invoice.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ItemService } from '../../../services/item.service';
import { ItemsComponent } from './Items/items.component';

@Component({
  selector: 'app-edit',
  templateUrl: './details.component.html',
})
export class DetailsComponent implements OnInit {
  public route: string = '/invoices';
  public items: any = {};
  invoice: any = {};
  public id: number;

  constructor(
    private router: Router,
    private toast: ToastrService,
    private invoiceService: InvoiceService,
    private itemService: ItemService,
    public bsModalRef: BsModalRef,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.invoiceService
        .get(this.id)
        .subscribe((response) => {
            this.invoice = response;
          },
          (err) => {
            this.toast.error("Erro ao buscar nota.", "Erro :(");
            this.router.navigate([this.route]);
          },
        );
  }

  getItems() {
    // this.itemService.getAllByInvoice(this.id)
    //                 .subscribe((response) => { 
    //                     this.items = response; 
    //                   },
    //                   (err) => {
    //                     this.toast.error("Erro ao buscar itens da nota.", "Erro :(");
    //                     this.router.navigate([this.route]);
    //                   },
    //                 );

    const initialState = {
      invoiceID: this.id
    };

    this.bsModalRef.hide();

    this.modalService.show(ItemsComponent, { initialState, animated: true });
  }
}
