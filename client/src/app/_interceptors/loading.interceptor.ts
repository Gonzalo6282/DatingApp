import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BusyService } from '../_services/busy.service';
import { environment } from '../../environments/environment';
import { delay, finalize, identity } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService);

  busyService.busy(); //start spinner

  return next(req).pipe(
    delay(1000),
    finalize(() => {
      busyService.idle();
    })
  );
};
