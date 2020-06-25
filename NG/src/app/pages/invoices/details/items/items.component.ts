import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ItemService } from '../../../../services/item.service';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
})
export class ItemsComponent implements OnInit {
  public invoiceID: number;
  items: any = [];
  public hideRuleContent:boolean[] = [];

  constructor(
    private toast: ToastrService,
    private itemService: ItemService,
    public bsModalRef: BsModalRef
  ) {}

  ngOnInit() {
    this.itemService.getAllByInvoice(this.invoiceID)
                    .subscribe((response) => { 
                        this.items = response; 
                        console.log(response);
                      },
                      (err) => {
                        this.toast.error("Erro ao buscar itens da nota.", "Erro :(");
                      },
                    );
  }

  toggle(index) {
    this.hideRuleContent[index] = !this.hideRuleContent[index];

    console.log(this.hideRuleContent[index]);
    console.log(index);
  }
}
