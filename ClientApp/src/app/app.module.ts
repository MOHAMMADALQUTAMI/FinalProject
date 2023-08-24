import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';

import { AuthGuard } from './guards/auth.guard';
import { MainPageComponent } from './web/main-page/main-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatIconModule } from '@angular/material/icon';
import { FooterComponent } from './web/footer/footer.component';
import { HeaderComponent } from './web/header/header.component';
import { NotFoundComponent } from './web/not-found/not-found.component';
import { ProductDetailComponent } from './web/product-detail/product-detail.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { LoginComponent } from './web/login/login.component';
import { RegisterComponent } from './web/register/register.component';


@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    FooterComponent,
    HeaderComponent,
    NotFoundComponent,
    ProductDetailComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    MatButtonModule,
    MatToolbarModule,
    MatMenuModule,
    MatCardModule,
    FlexLayoutModule,
    HttpClientModule,
    MatIconModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    RouterModule.forRoot([
      { path: '', canActivate: [AuthGuard], component: MainPageComponent, },
      { path: 'product', component: ProductDetailComponent, },
      { path: 'login', canActivate: [AuthGuard], component: LoginComponent, },
      { path: 'register', canActivate: [AuthGuard], component: RegisterComponent, },
      { path: '**', component: NotFoundComponent, },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
