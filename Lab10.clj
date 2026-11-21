;; ============================================
;; LAB 10
;; Experiment 17: Core Immutable Data Structures
;; Experiment 18: Sequence Transformations
;; ============================================


;; --------------------------------------------
;; EXPERIMENT 17: Clojure Core Immutable Data Structures
;; --------------------------------------------

(println "\n========== EXPERIMENT 17 ==========")


;; 1. LIST
(println "\n--- LIST ---")

(def original-list '(10 20 30 40))

(println "Original list:" original-list)
(println "First element:" (first original-list))
(println "Rest of list:" (rest original-list))

(def new-list (conj original-list 5))

(println "After conj:" new-list)
(println "Original list remains unchanged:" original-list)


;; 2. VECTOR
(println "\n--- VECTOR ---")

(def original-vector [10 20 30 40])

(println "Original vector:" original-vector)
(println "Element at index 2:" (get original-vector 2))
(println "Count:" (count original-vector))

(def new-vector (conj original-vector 50))

(println "After conj:" new-vector)
(println "Original vector remains unchanged:" original-vector)


;; 3. MAP
(println "\n--- MAP ---")

(def original-map {:name "Sumukhi"
                   :age 21
                   :course "CSE"})

(println "Original map:" original-map)
(println "Get name:" (get original-map :name))

(def updated-map (assoc original-map :age 22))

(println "After assoc:" updated-map)

(def removed-map (dissoc original-map :age))

(println "After dissoc:" removed-map)
(println "Original map remains unchanged:" original-map)


;; 4. SET
(println "\n--- SET ---")

(def original-set #{10 20 30 40})

(println "Original set:" original-set)
(println "Contains 20?" (contains? original-set 20))

(def added-set (conj original-set 50))

(println "After conj:" added-set)

(def removed-set (disj original-set 20))

(println "After disj:" removed-set)
(println "Original set remains unchanged:" original-set)


;; --------------------------------------------
;; EXPERIMENT 18: Sequence Transformations
;; map, filter, reduce, take, drop, threading
;; --------------------------------------------

(println "\n========== EXPERIMENT 18 ==========")


;; COLLECTION 1
(println "\n--- Collection 1 ---")

(def numbers1 [1 2 3 4 5 6 7 8 9 10])

(println "Original collection:" numbers1)

;; map: square each element
(println "Map (square):" (map #(* % %) numbers1))

;; filter: retain even numbers
(println "Filter (even numbers):" (filter even? numbers1))

;; reduce: calculate sum
(println "Reduce (sum):" (reduce + numbers1))

;; take: take first five elements
(println "Take 5:" (take 5 numbers1))

;; drop: remove first five elements
(println "Drop 5:" (drop 5 numbers1))

;; Threading macro ->>
;; Square numbers, filter even squares, and take first three
(println "Threading macro result:"
         (->> numbers1
              (map #(* % %))
              (filter even?)
              (take 3)))


;; COLLECTION 2
(println "\n--- Collection 2 ---")

(def numbers2 [11 12 13 14 15 16 17 18 19 20])

(println "Original collection:" numbers2)

;; map: double each element
(println "Map (double):" (map #(* % 2) numbers2))

;; filter: retain odd numbers
(println "Filter (odd numbers):" (filter odd? numbers2))

;; reduce: calculate maximum
(println "Reduce (maximum):" (reduce max numbers2))

;; take: take first four elements
(println "Take 4:" (take 4 numbers2))

;; drop: remove first four elements
(println "Drop 4:" (drop 4 numbers2))

;; Threading macro ->>
;; Double numbers, filter values greater than 30,
;; and take first three results
(println "Threading macro result:"
         (->> numbers2
              (map #(* % 2))
              (filter #(> % 30))
              (take 3)))


;; --------------------------------------------
;; END OF LAB 10
;; --------------------------------------------

(println "\nLab 10 completed successfully!")