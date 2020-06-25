import { Component, OnInit, TemplateRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FilteredItemService } from '../../../../services/filtered-item.service';
import { filter } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { DetailsComponent } from './details/details.component';

@Component({
  selector: 'app-filtered-items',
  templateUrl: './filtered-items.component.html',
})
export class FilteredItemsComponent implements OnInit {
  public filteredItems: any = [];
  public pagination: any = {};
  public filters: any = {
    page: 1,
    take: 5
  };

  private id;
  bsModalRef: BsModalRef;

  constructor(
    private toast: ToastrService,
    private filteredItemService: FilteredItemService,
    private modalService: BsModalService,
  ) {}

  ngOnInit() {
    //this.getUploadProcesses();
  }

  details(id){
    const initialState = {
      id: id
    };

    this.bsModalRef = this.modalService.show(DetailsComponent, { initialState });
  }

  getFilteredItems(processID) {
    this.id = processID;

    this.filteredItemService
        .getAllByProcessID(this.id, this.filters)
        .subscribe((response) => {
            this.filteredItems = response.filteredItems;
            this.pagination = response.pagination;
          },
          (err) => {
            //this.router.navigate([this.route]);
          }
        );
  }

  changePageSize(size) {
    this.filters.take = size;
    this.filters.page = 1;
    
    this.paginate(this.filters.page);
  }

  paginate(page: number) {
    this.filters.page = page;

    this.getFilteredItems(this.id);
  }
}
