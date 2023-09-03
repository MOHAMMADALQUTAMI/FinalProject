import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
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
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { PermissionCheckService } from './guards/permission-check.service';
import { CategoryComponent } from './web/admin/category/category.component';
import { CommonModule } from '@angular/common';
import { SubcategoryComponent } from './web/admin/subcategory/subcategory.component';
import { MyproductsComponent } from './web/shop/myproducts/myproducts.component';
import { ProductEditModalComponentComponent } from './web/shop/product-edit-modal-component/product-edit-modal-component.component';
import { MatDialogModule } from '@angular/material/dialog';
import { AuthorizationInterceptor } from './interceptor/authorization.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    FooterComponent,
    HeaderComponent,
    NotFoundComponent,
    ProductDetailComponent,
    LoginComponent,
    RegisterComponent,
    CategoryComponent,
    SubcategoryComponent,
    MyproductsComponent,
    ProductEditModalComponentComponent
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
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatSnackBarModule,
    MatCheckboxModule,
    MatDialogModule,
    RouterModule.forRoot([
      { path: '', component: MainPageComponent, },
      { path: 'product', component: ProductDetailComponent, },
      { path: 'login', canActivate: [AuthGuard], component: LoginComponent },
      {
        path: 'category', canActivate: [PermissionCheckService], component: CategoryComponent,
        data: { permissions: ['IsAdmin'] }
      }
      ,
      {
        path: 'subcategory', canActivate: [PermissionCheckService], component: SubcategoryComponent,
        data: { permissions: ['IsAdmin'] }
      }
      ,
      {
        path: 'myproducts', canActivate: [PermissionCheckService], component: MyproductsComponent,
        data: { permissions: ['IsShop'] }
      }
      ,
      { path: 'register', canActivate: [AuthGuard], component: RegisterComponent, },
      { path: '**', component: NotFoundComponent, },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizationInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
