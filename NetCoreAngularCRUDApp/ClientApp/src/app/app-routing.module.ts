import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlogPostsComponent } from './blog-posts/blog-posts.component';
import { BlogPostComponent } from './blog-post/blog-post.component';
import { BlogPostAddEditComponent } from './blog-post-add-edit/blog-post-add-edit.component';
import { OrdersComponent } from './orders/orders.component';

const routes: Routes = [
  { path: '', component: BlogPostsComponent, pathMatch: 'full' },
  { path: 'blogpost/add', component: BlogPostAddEditComponent },
  { path: 'blogpost/edit/:id', component: BlogPostAddEditComponent },
  { path: 'blogpost/view/:id', component: BlogPostComponent },
  { path: 'order', component: OrdersComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
