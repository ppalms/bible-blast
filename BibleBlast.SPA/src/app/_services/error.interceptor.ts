import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(catchError(response => {
            if (response instanceof HttpErrorResponse) {
                if (response.status === 401) {
                    return throwError(response.statusText);
                }

                const applicationException = response.headers.get('Application-Error');
                if (applicationException) {
                    return throwError(applicationException);
                }

                const serverError = typeof (response.error) === 'string' ? response.error : response.statusText;

                let modelStateErrors = '';
                if (response.error.errors) {
                    for (const key in response.error.errors) {
                        if (response.error.errors[key]) {
                            modelStateErrors += response.error.errors[key] + '\n';
                        }
                    }
                }

                return throwError(modelStateErrors || serverError || 'Server Error');
            }
        }));
    }
}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
