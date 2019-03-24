import { Injectable } from '@angular/core';
// this is made available in angular.json - scripts
// this lets tslint know that it shouldn't freak out
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  constructor() { }

  confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, (event) => {
      if (event) {
        okCallback();
      }
    });
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
