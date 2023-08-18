import { Component } from '@angular/core';
import { catchError, EMPTY, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestService } from 'src/app/guards/test.service';

@Component({
  selector: 'app-pagebody',
  templateUrl: './pagebody.component.html',
  styleUrls: ['./pagebody.component.scss']
})
export class PagebodyComponent implements OnInit {

  constructor(
    private serv: TestService,
    private httpsss: HttpClient,
    private router: Router,
    route: ActivatedRoute,
    @Inject('BASE_URL') private baseUrl: string
  ) {

  }



  ngOnInit(): void {
    this.getCategories();
  }

  getCategories(): void {
    this.serv.getCategories().subscribe(data => {
      console.log(data);
    });
  }

}
