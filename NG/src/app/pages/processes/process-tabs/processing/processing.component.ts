import { Component, OnInit, TemplateRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FilteredItemService } from '../../../../services/filtered-item.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-processing',
  templateUrl: './processing.component.html',
})
export class ProcessingComponent implements OnInit {
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
