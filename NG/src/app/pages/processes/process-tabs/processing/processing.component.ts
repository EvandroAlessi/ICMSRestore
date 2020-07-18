import { Component, OnInit, TemplateRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FilteredItemService } from '../../../../services/filtered-item.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-processing',
  templateUrl: './processing.component.html',
})
export class ProcessingComponent implements OnInit {
  public products: any = [];
  public pagination: any = {};

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

  getSumarry(processID) {
    this.id = processID;

    this.filteredItemService
        .getSumarry(this.id)
        .subscribe((response) => {
            this.products = response;
          },
          (err) => {
            //this.router.navigate([this.route]);
          }
        );
  }

  downLoadFile(data: any, type: string) {
    const blob = new Blob([data], { type: type});
    const url = window.URL.createObjectURL(blob);
    let pwa = window.open(url,"_self");

    if (!pwa || pwa.closed || typeof pwa.closed == 'undefined') {
        alert( 'Please disable your Pop-up blocker and try again.');
    }
  }

  buildFiles(ncm) {
    this.filteredItemService
        .download(this.id, ncm)
        .subscribe((response) => {
            this.toast.success('Arquivos baixados', 'Sucesso!');
            
            this.downLoadFile(response, 'application/zip');
          },
          (err) => {
            this.toast.error('Algo deu errado!', 'erro :(');
          }
        );
  }
}
