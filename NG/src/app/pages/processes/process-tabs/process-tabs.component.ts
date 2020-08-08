import { Component, OnInit, TemplateRef, ViewChild, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TabDirective } from 'ngx-bootstrap/tabs';
import { UploadComponent } from './upload/upload.component';
import { FilteredItemsComponent } from './filtered-items/filtered-items.component';
import { EditComponent } from './edit/edit.component';
import { ProcessingComponent } from './processing/processing.component';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ConverterService } from '../../../services/converter.service';
import { CreateComponent } from '../../converters/create/create.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-process-tabs',
  templateUrl: './process-tabs.component.html'
})
export class ProcessTabsComponent implements OnInit, AfterViewInit {
  public route: string = '/processes';
  public id: number;
  bsModalRef: BsModalRef;

  tabs: any[] = [
    { title: 'Dados', active: true },
    { title: 'Uploads' },
    { title: 'Itens Filtrados' },
    { title: 'Resumo' },
    { title: 'Resultados' },
  ];

  @ViewChild(EditComponent, { static: false}) edit: EditComponent;
  @ViewChild(UploadComponent, { static: false}) upload: UploadComponent;
  @ViewChild(FilteredItemsComponent, { static: false}) filteredItems: FilteredItemsComponent;
  @ViewChild(ProcessingComponent, { static: false}) processing: ProcessingComponent;
  
  constructor(
    private routerActived: ActivatedRoute,
    private converterService: ConverterService,
    private modalService: BsModalService,
    private toast: ToastrService,
  ) {}

  ngOnInit() {
    this.routerActived.params.subscribe((params) => {
      this.id = params.id;
    });
  }
  
  ngAfterViewInit(): void {
    this.edit.getProcess(this.id);
  }

  doWork(converter) {
    const initialState = {
      converter: converter,
      showCompanies: false
    };

    this.bsModalRef = this.modalService.show(CreateComponent, { initialState });
  }

  requiredConversions(processID) {
    this.converterService
        .getUnitDiferences(processID)
        .subscribe((response) => {
            console.log(response);

            if (response) {
              let first = true;

              response.forEach(converter => {
                
                if(!first) {
                  this.modalService
                    .onHidden
                    .subscribe(() => {
                      this.doWork(converter)
                    });
                }
                else {
                  this.doWork(converter)
                } 
              });
            }
          },
          (err) => {
            this.toast.error(err.errors, "Erro :(");
          }
        );
  }
  
  onSelect(data: TabDirective): void {
    switch (data.heading) {
      case this.tabs[0].title:
        this.edit.getProcess(this.id);
        break;
      case this.tabs[1].title:
        this.upload.getUploadProcesses(this.id);
        break;
      case this.tabs[2].title:
        this.filteredItems.getFilteredItems(this.id);
        break;
      case this.tabs[3].title:
        this.processing.getSumarry(this.id);
        this.requiredConversions(this.id);
        break;
    }
  }
}
