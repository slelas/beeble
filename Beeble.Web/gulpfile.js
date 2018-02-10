var gulp = require('gulp');
var sass = require('gulp-sass');
var cleanCSS = require('gulp-clean-css');
var rename = require('gulp-rename');
var plumber = require('gulp-plumber');

gulp.task('default', ['compileSass', 'watch']);

gulp.task('compileSass', function () {
    return gulp.src('./styles/main.scss')
        .pipe(plumber())
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('./dist/'))
        .pipe(cleanCSS())
        .pipe(rename('main.min.css'))
        .pipe(gulp.dest('./dist/'));
});

gulp.task('watch', function () {
    gulp.watch('./styles/**/*.scss', ['compileSass']);
});