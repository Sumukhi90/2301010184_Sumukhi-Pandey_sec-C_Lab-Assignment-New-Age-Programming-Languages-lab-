(ns Lab11)

;; ============================================================
;; LAB 11 - EXPERIMENT 20
;; JAVA INTEROPERABILITY IN CLOJURE
;; ============================================================

(println "========== EXPERIMENT 20 ==========")

;; 1. Calling Java static methods using dot notation
(println "\nJava Static Methods:")
(println "Absolute value of -25:" (Math/abs -25))
(println "Square root of 64:" (Math/sqrt 64))
(println "Current system time:" (System/currentTimeMillis))

;; 2. Creating Java objects
(println "\nCreating Java Objects:")

(def al (new java.util.ArrayList))
(def hm (new java.util.HashMap))

;; 3. Invoking instance methods
(.add al "Apple")
(.add al "Banana")
(.add al "Mango")

(println "ArrayList:" al)
(println "ArrayList size:" (.size al))
(println "First element:" (.get al 0))

(.put hm "Name" "Sumukhi")
(.put hm "Course" "Clojure")
(.put hm "Lab" "11")

(println "HashMap:" hm)
(println "Name from HashMap:" (.get hm "Name"))
(println "HashMap size:" (.size hm))

;; 4. Converting Clojure collections to Java collections
(println "\nCollection Conversion:")

(def clj-vector [10 20 30 40])
(def java-list (new java.util.ArrayList clj-vector))

(println "Clojure Vector:" clj-vector)
(println "Converted Java ArrayList:" java-list)

;; Converting Java collection back to Clojure
(def converted-vector (vec java-list))

(println "Java ArrayList converted to Clojure Vector:"
         converted-vector)

;; Converting Clojure map to Java HashMap
(def clj-map {:city "Delhi" :country "India"})
(def java-map (new java.util.HashMap))

(doseq [[k v] clj-map]
  (.put java-map k v))

(println "Clojure Map:" clj-map)
(println "Converted Java HashMap:" java-map)

;; Converting Java HashMap back to Clojure map
(def converted-map (into {} java-map))

(println "Java HashMap converted to Clojure Map:"
         converted-map)

(println "\nExperiment 20 Completed.")


;; ============================================================
;; LAB 11 - EXPERIMENT 23
;; EXCEPTION HANDLING AND ERROR MANAGEMENT IN CLOJURE
;; ============================================================

(println "\n========== EXPERIMENT 23 ==========")

;; 1. Function to throw a custom exception using ex-info
(defn check-age [age]
  (if (< age 18)
    (throw (ex-info "Age must be 18 or above"
                    {:age age
                     :error "Underage"}))
    (println "Age is valid.")))


;; 2. Handling ArithmeticException
(println "\nHandling ArithmeticException:")

(try
  (println "Result:" (/ 10 0))

  (catch ArithmeticException e
    (println "Error:" (.getMessage e))
    (println "Recovery: Division by zero is not allowed."))

  (finally
    (println "Arithmetic operation completed.")))


;; 3. Handling NullPointerException
(println "\nHandling NullPointerException:")

(try
  (let [text nil]
    (.length text))

  (catch NullPointerException e
    (println "Error: Null value encountered.")
    (println "Recovery: Using a default string instead.")
    (println "Default string length:" (.length "Default")))

  (finally
    (println "Null handling completed.")))


;; 4. Handling custom exception using ex-info
(println "\nHandling Custom Exception:")

(try
  (check-age 15)

  (catch clojure.lang.ExceptionInfo e
    (println "Exception Message:" (ex-message e))
    (println "Exception Data:" (ex-data e))
    (println "Recovery: Please enter a valid age.")
    (println "Error Type:" (:error (ex-data e)))
    (println "Invalid Age:" (:age (ex-data e))))

  (finally
    (println "Custom exception handling completed.")))


;; 5. Successful execution
(println "\nTesting Valid Age:")

(try
  (check-age 21)

  (catch clojure.lang.ExceptionInfo e
    (println "Error:" (ex-message e)))

  (finally
    (println "Age verification completed.")))


(println "\nLab 11 Completed Successfully.")