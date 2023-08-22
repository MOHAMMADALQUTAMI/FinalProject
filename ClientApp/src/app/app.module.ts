import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';

import { AuthGuard } from './guards/auth.guard';
import { MainPageComponent } from './web/main-page/main-page.component';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', canActivate: [AuthGuard], component: MainPageComponent, },
      { path: '**', component: AppComponent, },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
