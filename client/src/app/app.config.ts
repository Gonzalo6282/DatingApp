import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { provideToastr } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(), //add http to providers
    provideAnimations(), //add animations to providers
    provideToastr({//add toastr to providers and specify position where notification will show up
      positionClass: 'toast-bottom-right'
    }), 
  ],
};
