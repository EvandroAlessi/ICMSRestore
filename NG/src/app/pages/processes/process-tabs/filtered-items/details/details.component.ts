import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FilteredItemService } from '../../../../../services/filtered-item.service';

@Component({
  selector: 'app-edit',
  templateUrl: './details.component.html',
})
export class DetailsComponent implements OnInit {
  filteredItem: any = {};
  public id: number;

  constructor(
    private toast: ToastrService,
    private filteredItemService: FilteredItemService,
    public bsModalRef: BsModalRef
  ) {}

  ngOnInit() {
    this.filteredItemService
        .get(this.id)
        .subscribe((response) => {
            this.filteredItem = response;
          },
          (err) => {
            this.toast.error("Erro ao buscar item filtrado.", "Erro :(");
          },
        );
  }
}
