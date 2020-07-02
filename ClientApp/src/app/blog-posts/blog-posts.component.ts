import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blogpost';

@Component({
  selector: 'app-blog-posts',
  templateUrl: './blog-posts.component.html',
  styleUrls: ['./blog-posts.component.css']
})
export class BlogPostsComponent implements OnInit {
  blogPosts$: Observable<BlogPost[]>;

  constructor(private blogPostService: BlogPostService) {

  }

  ngOnInit(): void {
    this.loadBlogPosts();
  }

  loadBlogPosts(): void {
    this.blogPosts$ = this.blogPostService.getBlogPosts();
  }

  delete(postId): void {
    const confirmation = confirm('Do you want to delete blog post id: ' + postId + '?');
    if (confirmation) {
      this.blogPostService.deleteBlogPost(postId).subscribe((data) => {
        this.loadBlogPosts();
      });
    }
  }
}
