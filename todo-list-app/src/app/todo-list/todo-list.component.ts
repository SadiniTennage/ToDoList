import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent {
  tasks: { name: string }[] = [];
  newTask: string = '';
  currentDateAndTime: string | null = this.datePipe.transform(new Date(), 'yyyy-MM-dd') || null;
  
  constructor(private http: HttpClient, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.fetchTasks();
  }

  fetchTasks(): void {
    this.http.get<any[]>('https://localhost:5000/api/tasks')
      .subscribe(tasks => {
        this.tasks = tasks;
      });
  }

  addTask(): void {
    if (this.newTask.trim() !== '') {
      this.http.post<any>('https://localhost:5000/api/tasks', { name: this.newTask })
        .subscribe(task => {
          this.tasks.push(task);
          this.newTask = ''; // Clear input field after adding task
        });
    }
  }

  deleteTask(task: any): void {
    this.http.delete(`https://localhost:5000/api/tasks/${task.id}`)
      .subscribe(() => {
        this.tasks = this.tasks.filter(t => t !== task);
      });
  }
}
