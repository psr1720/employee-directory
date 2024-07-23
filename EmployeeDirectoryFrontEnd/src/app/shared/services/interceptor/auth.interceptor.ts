import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  let token = localStorage.getItem('AuthToken');
  if (token) {
      let clonedRequest = req.clone({
          headers: req.headers.set('Authorization', `Bearer ${token}`)
      });
      return next(clonedRequest);
  }
  else {
      return next(req);
  }
};
