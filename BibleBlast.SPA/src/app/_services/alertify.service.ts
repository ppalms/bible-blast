import { Injectable } from '@angular/core';
// this is made available in angular.json - scripts
// this lets tslint know that it shouldn't freak out
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  constructor() { }

  confirm(title: string, message: string, okCallback: () => any) {
    alertify.confirm(title, message, (event) => {
      if (event) {
        okCallback();
      }
    }, null);
  }

  success(message: string) {
    alertify.success(message);
  }

  error(message: string) {
    alertify.error(message);
  }

  warning(message: string) {
    alertify.warning(message);
  }

  message(message: string) {
    alertify.message(message);
  }
}
