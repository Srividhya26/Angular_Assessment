import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError} from 'rxjs/operators';
import { feedbackUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  //headers = new HttpHeaders().set('Content-Type','application/json');
  headers = new HttpHeaders().set('Content-Type','multipart/form-data');

  constructor(private http : HttpClient, private router : Router) { }

  createFeedback(data : any) : Observable<any>{
    return this.http.post(feedbackUrl +'api/Feedback/CreateFeedback',data).pipe(
      catchError(this.handleError)
    )
  }

  listCategory() : Observable<any>{
      return this.http.get(feedbackUrl + 'api/Feedback/GetAllCategory').pipe(
        catchError(this.handleError)
      )
  }

  listReview() : Observable<any>{
    return this.http.get(feedbackUrl + 'api/Feedback/GetReviews').pipe(
      catchError(this.handleError)
    )
  }

  getProductByCategory(value = 0) : Observable<any>
  {
    return this.http.get(feedbackUrl + 'api/Feedback/GetProductsByCategory' +"?id="+value).pipe(
      catchError(this.handleError)
    );
  }

  getFeedback(text : string) : Observable<any>
  {
    return this.http.get(feedbackUrl + 'api/Feedback/GetFeedback' + "?encryptedText="+text).pipe(
      catchError(this.handleError)
    );
  }

  handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return throwError(
      'Something bad happened; please try again later.');
    };

  }

