import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProcessTabsComponent } from './process-tabs.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { UploadModule } from './upload/upload.module';
import { FilteredItemsModule } from './filtered-items/filtered-items.module';
import { EditModule } from './edit/edit.module';
import { ProcessTabsRouting } from './process-tabs.routing';
import { ProcessingModule } from './processing/processing.module';
import { CreateComponent } from '../../converters/create/create.component';
import { CreateModule } from '../../converters/create/create.module';

@NgModule({
  declarations: [ProcessTabsComponent],
  imports: [
    CommonModule, 
    RouterModule,
    FormsModule, 
    TabsModule,

    //APP
    ProcessTabsRouting,
    UploadModule,
    FilteredItemsModule,
    EditModule,
    ProcessingModule,
    CreateModule,
  ],
  entryComponents: [
    CreateComponent
  ]
})
export class ProcessTabsModule {}
