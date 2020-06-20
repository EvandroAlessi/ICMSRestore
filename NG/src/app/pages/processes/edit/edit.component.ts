import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProcessService } from '../../../services/process.service';
import { UploadProcessService } from '../../../services/upload-processes.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FilteredItemService } from '../../../services/filtered-item.service';
import { UploadService } from '../../../services/upload.service';

@Component({
  selector: 'app-edit',
  styleUrls: ['./edit.component.scss'],
  templateUrl: './edit.component.html',
})
export class EditComponent implements OnInit {
  public route: string = '/processes';
  public errors: any = {};
  public process: any = {};
  public uploadProcesses: any = [];
  public filteredItems: any = [];
  public id: number;
  modalRef: BsModalRef;
  
  constructor(
    private modalService: BsModalService,
    private routerActived: ActivatedRoute,
    private router: Router,
    private toast: ToastrService,
    private processService: ProcessService,
    private uploadProcessService: UploadProcessService,
    private filteredItemService: FilteredItemService,
    private uploadService: UploadService,
  ) {}

  ngOnInit() {
    this.routerActived.params.subscribe((params) => {
      this.id = params.id;

      this.processService
          .get(this.id)
          .subscribe((response) => {
              this.process = response;
            },
            (err) => {
              this.router.navigate([this.route]);
            },
          );
    });

    this.getUploadProcesses();
    this.getFilteredItems();
  }
  
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  getUploadProcesses() {
    this.uploadProcessService
        .getAllByProcessID(this.id)
        .subscribe((response) => {
          this.uploadProcesses = response;

            for (let i = 0; i < this.uploadProcesses.length; i++) {
              const element = this.uploadProcesses[i];

              if(element.ativo){
                setInterval(() => {
                  this.uploadProcessService.getState(element.id).subscribe((response) => {
                    element.percent = response.percent;
                    element.errorFiles = response.errorFiles;
                  });
                }, 5000);
              }
            }
          },
          (err) => {
            this.router.navigate([this.route]);
          }
        );
  }

  getFilteredItems() {
    this.filteredItemService
        .getAllByProcessID(this.id)
        .subscribe((response) => {
            this.filteredItems = response.filteredItems;
          },
          (err) => {
            this.router.navigate([this.route]);
          }
        );
  }

  save() {
    this.processService
        .put(this.id, this.process)
        .subscribe((response) => {
            this.toast.success('Processo editado.', 'Sucesso!');
            this.router.navigate([this.route]);
          },
          (error) => {
            this.errors = error.error;
          },
        );
  }

  cancel() {
    this.router.navigate([this.route]);
  }


  onConfirm(){
    let files = this.files;

    files.forEach(file => {
      this.files = this.files.filter(obj => obj !== file);

      this.uploadService
        .post(this.id, file)
        .subscribe((response) => {
            this.getUploadProcesses();
          },
          (err) => {
            alert('Fodeu');
          }
        );
    });
  }




  files: any[] = [];

  /**
   * on file drop handler
   */
  onFileDropped($event) {
    this.prepareFilesList($event);
  }

  /**
   * handle file from browsing
   */
  fileBrowseHandler(files) {
    this.prepareFilesList(files);
  }

  /**
   * Delete file from files list
   * @param index (File index)
   */
  deleteFile(index: number) {
    this.files.splice(index, 1);
  }

  /**
   * Simulate the upload process
   */
  uploadFilesSimulator(index: number) {
    setTimeout(() => {
      if (index === this.files.length) {
        return;
      } else {
        const progressInterval = setInterval(() => {
          if (this.files[index].progress === 100) {
            clearInterval(progressInterval);
            this.uploadFilesSimulator(index + 1);
          } else {
            this.files[index].progress += 5;
          }
        }, 200);
      }
    }, 1000);
  }

  /**
   * Convert Files list to normal array list
   * @param files (Files List)
   */
  prepareFilesList(files: Array<any>) {

    for (const item of files) {
      if (item.type == "application/x-zip-compressed") {
        item.progress = 0;
        this.files.push(item);
      }
    }
    this.uploadFilesSimulator(0);
  }

  /**
   * format bytes
   * @param bytes (File size in bytes)
   * @param decimals (Decimals point)
   */
  formatBytes(bytes, decimals) {
    if (bytes === 0) {
      return '0 Bytes';
    }
    const k = 1024;
    const dm = decimals <= 0 ? 0 : decimals || 2;
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
  }
}
