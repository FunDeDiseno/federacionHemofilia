/// <binding Clean='clean' />
"use strict";
var gulp = require("gulp"),
  rimraf = require("rimraf"),
  concat = require("gulp-concat"),
  cssmin = require("gulp-cssmin"),
  uglify = require("gulp-uglify"),
  project = require("./project.json"),
  less = require("gulp-less"),
  path = require("path");

var paths = {
    npm: "./node_modules/",
    webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.less = paths.webroot + "less/**/*.less";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.minLess = paths.webroot + "css/base/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";
paths.concatVariablesCssDest = paths.webroot + "css/base/_variables.min.css";
paths.concatGraphCssDest = paths.webroot + "css/components/_graph.min.css";

gulp.task("less", function () {
    return gulp.src(paths.less)
               .pipe(less())
               .pipe(gulp.dest(paths.webroot+"css/"));
});

gulp.task("compileLess", ["less"]);

gulp.task("clean:js", function(cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function(cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function() {
    gulp.src([paths.js, "!" + paths.minJs], {
        base: "."
    })
      .pipe(concat(paths.concatJsDest))
      .pipe(uglify())
      .pipe(gulp.dest("."));
});

gulp.task("min:css", function() {
    gulp.src([paths.css, "!" + paths.minCss])
      .pipe(concat(paths.concatCssDest))
      .pipe(cssmin())
      .pipe(gulp.dest("."));
});

gulp.task("min:variables", function () {
    gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatVariablesCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:graph", function () {
    gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatGraphCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});


gulp.task("min", ["min:js", "min:css", "min:variables", "min:graph"]);
