import { Component, OnInit, TemplateRef, OnDestroy } from '@angular/core';
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
export class UploadComponent implements OnInit, OnDestroy {
  public uploadProcesses: any = [];
  private processID: number;
  private entrada: boolean = false;
  private canSend: boolean = true;

  private timersID = [];

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

  ngOnDestroy() {
    if(this.timersID.length > 0) {
      this.timersID.forEach(element => {
        clearInterval(element);
      });

      this.timersID = null;
    }
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  getUploadProcesses(processID) {
    this.processID = processID;

    if(this.timersID.length > 0) {
      this.timersID.forEach(element => {
        clearInterval(element);
      });

      this.timersID = [];
    }

    this.uploadProcessService
        .getAllByProcessID(this.processID, this.filters)
        .subscribe((response) => {
          this.uploadProcesses = response.uploadProcesses;
          this.pagination = response.pagination;

            for (let i = 0; i < this.uploadProcesses.length; i++) {
              const element = this.uploadProcesses[i];

              if(element.ativo){
                setInterval(() => {
                  this.timersID[i] = this.uploadProcessService
                    .getState(element.id)
                    .subscribe((response) => {
                      element.percent = response.percent;
                      element.errorFiles = response.errorFiles;
                    },
                    (err) => {
                      clearInterval(this.timersID[i]);
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

  downLoadFile(data: any, type: string) {
    const blob = new Blob([data], { type: type});
    const url = window.URL.createObjectURL(blob);
    let pwa = window.open(url,"_self");

    if (!pwa || pwa.closed || typeof pwa.closed == 'undefined') {
        alert( 'Please disable your Pop-up blocker and try again.');
    }
  }

  downloadErros(id) {
    this.uploadProcessService
        .download(id)
        .subscribe((response) => {
            this.toast.success('Arquivos baixados', 'Sucesso!');
            
            this.downLoadFile(response, 'application/zip');
          },
          (err) => {
            this.toast.error('Algo deu errado!', 'erro :(');
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

    this.getUploadProcesses(this.processID);
  }

  onConfirm(){
    if(this.files.length > 0)
    {
      let error = 0;
      this.canSend = false;

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
                this.toast.success("Arquivo '"+ file.name +"' enviado.", 'Sucesso!');
                this.files = this.files.filter(obj => obj !== file);
  
                console.log("i: " + i);
                console.log("length: " + this.files.length);

                if(this.files.length == 0) {
                  this.canSend = true;
                }
              }
            },
            (err) => {
              this.toast.error("Não foi possível enviar o arquvo '"+ file.name +"'.", 'Erro :(');
              error++;

              if(this.files.length - error == 0) {
                this.canSend = true;
              }
            }
          );
      });
    }
    else {
      this.toast.info("Primeiro você precisa selecionar algum documento.", ";)");
    }
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
      else {
        this.toast.warning("Só é possível enviar arquivos do tipo ZIP. Qualquer outro tipo, mesmo XML ou RAR não é aceito.", 'Atenção');
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
