import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  private baseUrl: string;  
  public shortUrl: string;
  public ShortUrlForm: any;

  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  GetShortUrl(form: NgForm) {
    const payload = form.value;
    this.http.post(this.baseUrl + 'Url/add-url', payload).subscribe(result => {
      this.shortUrl = result.toString();
    },
      err => { console.log("Task controller says: " + Error) }
    );    
  }

}
export class Urlscr {
  id: string;
  longUrl: string;
  shortUrl: string;
}
