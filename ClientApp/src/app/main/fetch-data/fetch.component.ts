import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
    selector: 'app-sub-categories-list',
    templateUrl: './fetch.component.html'
})

export class fetchComponent implements OnInit {
    items = '';
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    ngOnInit(): void {
        this.http.get<any>(this.baseUrl + 'api/food/Getdata').subscribe(result => {
            this.items = result;
            console.log(result);
        });
    }

}