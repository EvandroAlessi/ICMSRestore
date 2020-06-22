import { Component, OnInit, TemplateRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UploadProcessService } from '../../../../services/upload-processes.service';
import { UploadService } from '../../../../services/upload.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { HttpEventType, HttpEvent } from '@angular/common/http';

@Component({
  selector: 'app-upload',
  styleUrls: ['./upload.component.scss'],
  templateUrl: './upload.component.html',
})
export class UploadComponent implements OnInit {
  public uploadProcesses: any = [];
  private processID: number;
  private entrada: boolean = false;
  private canSend: boolean = true;

  public modalRef: BsModalRef; 

  public pagination: any = {};
  public filters: any = {
    page: 1,
    take: 5
  };

  constructor(
    private toast: ToastrService,
    private uploadService: UploadService,
    private uploadProcessService: UploadProcessService,
    private modalService: BsModalService,
  ) {}

  ngOnInit() {
    //this.getUploadProcesses();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  getUploadProcesses(processID) {
    this.processID = processID;

    this.uploadProcessService
        .getAllByProcessID(this.processID, this.filters)
        .subscribe((response) => {
          this.uploadProcesses = response.uploadProcesses;
          this.pagination = response.pagination;

            for (let i = 0; i < this.uploadProcesses.length; i++) {
              const element = this.uploadProcesses[i];

              if(element.ativo){
                setInterval(() => {
                  this.uploadProcessService.getState(element.id).subscribe((response) => {
                    element.percent = response.percent;
                    element.errorFiles = response.errorFiles;
                  });
                }, 20000);
              }
            }
          },
          (err) => {
            //this.router.navigate([this.route]);
          }
        );
  }

  changePageSize(size) {
    let take = (Number)(this.filters.take);
    let page = (Number)(this.filters.page);

    let lastItem = (Number)(page*take);

    if(lastItem > size) {
      this.filters.page = Math.floor(lastItem / size);
    }

    this.filters.take = size;
    this.paginate(this.filters.page);
  }

  paginate(page: number) {
    this.filters.page = page;

    this.getUploadProcesses(this.processID);
  }

  onConfirm(){
    this.canSend = false;
    // let files = this.files;

    this.files.forEach((file, i)=> {
      this.uploadService
        .post(file, this.processID, this.entrada)
        .subscribe((event) => {
            // progress
            if (event.type === HttpEventType.UploadProgress) {
              const percentage = 100 / event.total * event.loaded;
              file.progress = percentage;
            }
          
            // finished
            if (event.type === HttpEventType.Response) {
              this.getUploadProcesses(this.processID);
              this.toast.success("Arquivos enviados.", 'Sucesso!');
              this.files = this.files.filter(obj => obj !== file);

              if(i == this.files.length) {
                this.canSend = true;
              }
            }
          },
          (err) => {
            this.toast.error("Não foi possível enviar os arquivos.", 'Erro :(');
            //this.modalService.hide(0);
          }
        );
    });
  }

  files: any[] = [];

  /**
   * on file drop handler
   */
  onFileDropped($event) {
    if (this.canSend) {
      this.prepareFilesList($event);
    }
    else {
      this.toast.info("Você tem que esparar os arquivos serem enviados para nosso servidor.", 'Info');
    }
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
    if (this.canSend) {
      this.files.splice(index, 1);
    }
    else {
      this.toast.info("Você tem que esparar os arquivos serem enviados para nosso servidor.", 'Info');
    }
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
    //this.uploadFilesSimulator(0);
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
