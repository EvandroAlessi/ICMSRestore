import { Component, OnInit, TemplateRef, ViewChild, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TabDirective } from 'ngx-bootstrap/tabs';
import { UploadComponent } from './upload/upload.component';
import { FilteredItemsComponent } from './filtered-items/filtered-items.component';
import { EditComponent } from './edit/edit.component';
import { ProcessingComponent } from './processing/processing.component';

@Component({
  selector: 'app-process-tabs',
  templateUrl: './process-tabs.component.html'
})
export class ProcessTabsComponent implements OnInit, AfterViewInit {
  public route: string = '/processes';
  public id: number;

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
  ) {}

  ngOnInit() {
    this.routerActived.params.subscribe((params) => {
      this.id = params.id;
    });
  }
  
  ngAfterViewInit(): void {
    this.edit.getProcess(this.id);
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
        break;
    }
  }
}
